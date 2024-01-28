import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IChatPreview, IMessage } from '../_interfaces/IChat';
import { environment } from 'src/environments/environment';

const HUB_URL: string = `${environment.hubUrl}`;

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection

  newChatPreview$ = new Subject<IChatPreview>();
  newMyChatPreview$ = new Subject<IChatPreview>();
  newMessage$ = new Subject<IMessage>();

  constructor() { }

  async startConnection(token: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${HUB_URL}/chat-hub`, {accessTokenFactory: () => token})
      .withAutomaticReconnect()
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log("Connected to chat hub");
      })
      .catch(err => console.log(err));

    this.receiveChatPreview();
    this.receiveMyChatPreview();
    this.receiveMessage();
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

  receiveMessage() {
    this.hubConnection?.on("ReceiveMessage", message => {
      this.newMessage$.next(message);
    });
  }

}
