import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { AccountService } from '../account.service';
import { Subject } from 'rxjs';
import { INotification } from 'src/app/_interfaces/INotification';

const BASE_URL: string = `${environment.hubUrl}/notification`

@Injectable({
  providedIn: 'root'
})
export class NotificationHubService {
  private hubConnection?: HubConnection;
  newNotification$ = new Subject<INotification>();

  constructor(
    private accountService: AccountService
  ) { }

  onStartConnection() {
    const token = this.accountService.getToken();
    if (!token) return;
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${BASE_URL}`, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().then(_ => console.log('Successfully connected to notification'))
      .catch(err => console.log(err));

    this.onNewNotification();
  }

  onNewNotification() {
    this.hubConnection?.on("NewNotification", notification => {
      this.newNotification$.next(notification);
    });
  }

  onStopConnection() {
    this.hubConnection?.stop()
      .then(_ => console.log("Notification connection stop successfully."))
      .catch(err => console.log(err));
  }

}
