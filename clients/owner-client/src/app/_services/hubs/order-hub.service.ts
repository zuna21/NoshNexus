import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IOrderCard } from 'src/app/_interfaces/IOrder';
import { environment } from 'src/environments/environment';

const HUB_URL: string = `${environment.hubUrl}`;

@Injectable({
  providedIn: 'root'
})
export class OrderHubService {
  private hubConnection?: HubConnection;
  newOrder$ = new Subject<IOrderCard>();

  constructor() { }

  async startConnection(token: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${HUB_URL}/order-hub`, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log("Connected to order hub");
      })
      .catch(err => console.log(err));

    this.receiveOrder();
  }

  stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => {
          console.log("Unconnected from order hub");
        })
        .catch(err => console.error(err));
    }
  }

  receiveOrder() {
    this.hubConnection?.on("ReceiveOrder", order => {
      this.newOrder$.next(order);
    });
  }
}
