import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IChatPreview } from '../_interfaces/IChat';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection

  newChatPreview$ = new Subject<IChatPreview>();
  newMyChatPreview$ = new Subject<IChatPreview>();

  constructor() { }

  async startConnection(token: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/chat-hub', {accessTokenFactory: () => token})
      .withAutomaticReconnect()
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log("Connected to chat hub");
      })
      .catch(err => console.log(err));

    this.receiveChatPreview();
    this.receiveMyChatPreview();
  }

  stopConnection() {
    if (this.hubConnection) {
      this.hubConnection.stop()
        .then(() => {
          console.log("Unconnected from chat hub");
        })
        .catch(err => console.error(err));
    }
  }

  receiveChatPreview() {
    this.hubConnection?.on("ReceiveChatPreview", chatPreview => {
      this.newChatPreview$.next(chatPreview);
    });
  }

  receiveMyChatPreview() {
    this.hubConnection?.on("ReceiveMyChatPreview", chatPreview => {
      this.newMyChatPreview$.next(chatPreview);
    })
  }
}
