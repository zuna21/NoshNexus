import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { IOrderCard } from '../_interfaces/IOrder';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderHubService {
  private hubConnection?: HubConnection;

  newOrder$ = new Subject<IOrderCard>();

  constructor() { }

  async startConnection(token: string, restaurantId: number): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/order-hub', {accessTokenFactory: () => token})
      .withAutomaticReconnect()
      .build();

    await this.hubConnection.start()
      .then(() => {
        console.log("Connected to order hub");
      })
      .catch(err => console.error(err));

    this.hubConnection.invoke("JoinGroup", restaurantId);
    this.receiveOrder();
  }

  async stopConnection(restaurantId: number) {
    if (this.hubConnection) {
      await this.hubConnection.invoke("LeaveGroup", restaurantId);
      this.hubConnection.stop()
        .then(() => {
          console.log("Unconnected from order hub");
        })
        .catch(err => console.error(err));
    }
  }

  receiveOrder() {
    this.hubConnection?.on("ReceiveOrder", newOrder => {
      this.newOrder$.next(newOrder);
    });
  }


}
