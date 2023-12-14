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
  acceptedOrderId$ = new Subject<number>();

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
    this.acceptOrder();
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
    this.hubConnection?.on("ReceiveOrder", newOrder => {
      this.newOrder$.next(newOrder);
    });
  }

  acceptOrder() {
    this.hubConnection?.on("AcceptOrder", orderId => {
      this.acceptedOrderId$.next(orderId);
    });
  }


}
