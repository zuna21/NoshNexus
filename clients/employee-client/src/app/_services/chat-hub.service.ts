import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IChatPreview } from '../_interfaces/IChat';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection;

  newChatPreview$ = new Subject<IChatPreview>();

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
    this.receiveChatPreview();
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

  receiveChatPreview() {
    this.hubConnection?.on("ReceiveChatPreview", (chatPreview: IChatPreview) => {
      this.newChatPreview$.next(chatPreview);
    });
  }

}
