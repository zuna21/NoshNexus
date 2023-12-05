import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection;

  constructor() { }

  async startConnection(username: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/chatHub')
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log('SignalR chat connection started.');
      })
      .catch(err => {
        console.error('Error starting SignalR connection:', err);
      });

    this.hubConnection.invoke('JoinGroups', username);
  }


  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => {
          console.log('SignalR chat connection stopped.');
        })
        .catch(err => {
          console.error('Error stopping SignalR connection:', err);
        });
    }
  }

}
