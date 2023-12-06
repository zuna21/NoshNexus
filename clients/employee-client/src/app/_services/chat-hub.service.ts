import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { IChatPreview, IMessage } from '../_interfaces/IChat';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection;

  newMessage$ = new Subject<IMessage>();
  newMyMessage$ = new Subject<IMessage>();
  newChatPreview$ = new Subject<IChatPreview>();

  constructor(
  ) { }

  async startConnection(token: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/chatHub', { accessTokenFactory: () => token })
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log('SignalR chat connection started.');
      })
      .catch(err => {
        console.error('Error starting SignalR connection:', err);
      });

    this.hubConnection.invoke("JoinGroups");
    this.receiveChatPreview();
    this.receiveMessage();
    this.receiveMyMessage();
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


  sendMessage(chatId: number, chat: {content: string}) {
    this.hubConnection?.invoke("SendMessage", chatId, chat)
      .then(() => console.log('Message send'))
      .catch(error => console.log(error));
  }

  receiveMyMessage() {
    this.hubConnection?.on("ReceiveMyMessage", (message: IMessage) => {
      this.newMyMessage$.next(message);
    })
  }

  receiveMessage() {
    this.hubConnection?.on("ReceiveMessage", (message: IMessage) => {
      this.newMessage$.next(message);
    })
  }

  receiveChatPreview() {
    this.hubConnection?.on("ReceiveChatPreview", (chatPreview: IChatPreview) => {
      this.newChatPreview$.next(chatPreview);
    });
  }

}
