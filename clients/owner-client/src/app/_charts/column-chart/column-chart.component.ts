import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';

@Component({
  selector: 'app-column-chart',
  standalone: true,
  imports: [
    CommonModule,
    CanvasJSAngularChartsModule
  ],
  templateUrl: './column-chart.component.html',
  styleUrls: ['./column-chart.component.css']
})
export class ColumnChartComponent {
  chart: any;
	
  chartOptions = {
    title:{
      text: "Narudzbe po danu"
    },
    theme: "dark2",
    animationEnabled: true,
    axisY: {
      includeZero: true,
    },
    data: [{
      type: "bar",
      indexLabel: "{y}",
      dataPoints: [
        { label: "Ponedeljak", y: 15 },
        { label: "Utorak", y: 20 },
        { label: "Srijeda", y: 24 },
        { label: "Cetvrtak", y: 29 },
        { label: "Petak", y: 41 },
        { label: "Subota", y: 25 },
        { label: "Nedelja", y: 31 },
      ]
    }]
  }	
}
