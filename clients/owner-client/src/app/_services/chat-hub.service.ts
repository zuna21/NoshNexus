import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private hubConnection?: HubConnection

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
}
