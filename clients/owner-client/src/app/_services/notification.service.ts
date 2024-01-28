import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { INotification, INotificationsForMenu } from '../_interfaces/INotification';
import { environment } from 'src/environments/environment';

const OWNER_URL: string = `${environment.apiUrl}/owner/notifications`;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerNotificationsForMenu(): Observable<INotificationsForMenu> {
    return this.http.get<INotificationsForMenu>(`${OWNER_URL}/get-notifications-for-menu`);
  }

  getAllNotifications(): Observable<INotification[]> {
    return this.http.get<INotification[]>(`${OWNER_URL}/get-all-notifications`);
  }

  markNotificationAsRead(notificationId: number): Observable<number> {
    return this.http.get<number>(`${OWNER_URL}/mark-notification-as-read/${notificationId}`);
  }

  markAllNotificationsAsRead(): Observable<boolean> {
    return this.http.get<boolean>(`${OWNER_URL}/mark-all-notifications-as-read`);
  }
}
