import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';
import jsPDF from 'jspdf';
import { PdfWrapperComponent } from 'src/app/_components/pdf-wrapper/pdf-wrapper.component';
import { PdfService } from 'src/app/_components/pdf-wrapper/pdf.service';

@Component({
  selector: 'app-tables-qr-code',
  standalone: true,
  imports: [
    CommonModule,
    QRCodeModule,
    PdfWrapperComponent
  ],
  templateUrl: './tables-qr-code.component.html',
  styleUrls: ['./tables-qr-code.component.css']
})
export class TablesQrCodeComponent {

  constructor(
    private pdfService: PdfService
  ) {}

  onDownload() {
    this.pdfService.onDownload();
  }
}
