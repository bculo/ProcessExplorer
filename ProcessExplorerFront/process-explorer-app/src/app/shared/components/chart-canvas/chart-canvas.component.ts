import { Component, OnInit, Input } from '@angular/core';
import { IChartExtendedModel, ILoadingMember } from '../../models/interfaces.models';

@Component({
  selector: 'app-chart-canvas',
  templateUrl: './chart-canvas.component.html',
  styleUrls: ['./chart-canvas.component.css']
})
export class ChartCanvasComponent implements OnInit {

  @Input() chart: ILoadingMember<IChartExtendedModel>

  constructor() { }

  ngOnInit(): void {
  }

}
