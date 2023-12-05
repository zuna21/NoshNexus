import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable, Subject, of } from 'rxjs';
import { INotification } from '../_interfaces/INotification';

@Injectable({
  providedIn: 'root'
})
export class NotificationHubService {
  private hubConnection?: HubConnection;

  constructor() { }

  startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/notificationHub')
      .build();

    this.hubConnection.start()
      .then(() => {
        console.log('SignalR connection started.');
      })
      .catch(err => {
        console.error('Error starting SignalR connection:', err);
      });
  }


  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => {
          console.log('SignalR connection stopped.');
        })
        .catch(err => {
          console.error('Error stopping SignalR connection:', err);
        });
    }
  }


  receiveNotificationForMenu(): Observable<INotification> {
    return new Observable<INotification>(observer => {
      this.hubConnection?.on('GetNewNotification', (notification: INotification) => {
        observer.next(notification);
      });
    });
  }

}
