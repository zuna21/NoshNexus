import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { INotification, INotificationsForMenu } from '../_interfaces/INotification';

const BASE_URL: string = `${environment.apiUrl}/notification`;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerNotificationsForMenu(): Observable<INotificationsForMenu> {
    return this.http.get<INotificationsForMenu>(`http://localhost:5000/api/owner/notifications/get-notifications-for-menu`);
  }

  getOwnerNotifications(): Observable<INotification[]> {
    return this.http.get<INotification[]>(`${BASE_URL}/get-owner-notifications`);
  }
}
