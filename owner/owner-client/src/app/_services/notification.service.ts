import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { INotificationsForMenu } from '../_interfaces/INotification';

const BASE_URL: string = `${environment.apiUrl}/notification`;

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(
    private http: HttpClient
  ) { }

  getOwnerNotifications(): Observable<INotificationsForMenu> {
    return this.http.get<INotificationsForMenu>(`${BASE_URL}/get-owner-notifications-for-menu`);
  }
}
