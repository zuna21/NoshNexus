import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatChipsModule} from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';


@Component({
  selector: 'app-restaurants-edit',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatIconModule, MatSlideToggleModule, MatChipsModule, MatButtonModule],
  templateUrl: './restaurants-edit.component.html',
  styleUrls: ['./restaurants-edit.component.css']
})
export class RestaurantsEditComponent {

}

