import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';
import jsPDF from 'jspdf';
import { PdfWrapperComponent } from 'src/app/_components/pdf-wrapper/pdf-wrapper.component';
import { PdfService } from 'src/app/_components/pdf-wrapper/pdf.service';
import { ActivatedRoute } from '@angular/router';
import { ITable } from 'src/app/_interfaces/ITable';
import { TableService } from 'src/app/_services/table.service';
import { Subscription } from 'rxjs';
import { TableQrCodeComponent } from './table-qr-code/table-qr-code.component';

@Component({
  selector: 'app-tables-qr-code',
  standalone: true,
  imports: [
    CommonModule,
    QRCodeModule,
    TableQrCodeComponent,
    PdfWrapperComponent
  ],
  templateUrl: './tables-qr-code.component.html',
  styleUrls: ['./tables-qr-code.component.css']
})
export class TablesQrCodeComponent implements OnInit, OnDestroy {
  restaurantId?: number;
  tables: ITable[] = [];

  tableSub?: Subscription;

  constructor(
    private pdfService: PdfService,
    private activatedRoute: ActivatedRoute,
    private tableService: TableService
  ) {}

  ngOnInit(): void {
    this.getTables();
  }

  getTables() {
    this.restaurantId = parseInt(this.activatedRoute.snapshot.params['restaurantId']);
    if (!this.restaurantId) return;
    this.tableSub = this.tableService.getAllRestaurantTableNames(this.restaurantId).subscribe({
      next: tables => {
        this.tables = [...tables];
      }
    });
  }

  onDownload() {
    this.pdfService.onDownload();
  }

  ngOnDestroy(): void {
    this.tableSub?.unsubscribe();
  }
}
