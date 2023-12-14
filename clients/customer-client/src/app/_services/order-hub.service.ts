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
  removedOrderId$ = new Subject<number>();
  declineOrder$ = new Subject<{id: number; reason: string;}>();

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
    this.removeOrder();
    this.declineOrder();
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

  declineOrder() {
    this.hubConnection?.on("DeclineOrder", declinedOrder => {
      this.declineOrder$.next({
        id: declinedOrder.id,
        reason: declinedOrder.reason
      });
    });
  }

  removeOrder() {
    this.hubConnection?.on("RemoveOrder", orderId => {
      this.removedOrderId$.next(orderId);
    });
  }


}
