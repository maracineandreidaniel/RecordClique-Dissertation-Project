import { Component } from '@angular/core';
import { StatisticsService } from 'src/services/statistics/statistics.service';

@Component({
  selector: 'app-reporting-page',
  templateUrl: './reporting-page.component.html',
  styleUrls: ['./reporting-page.component.css']
})
export class ReportingPageComponent {

  constructor(private statisticService: StatisticsService){

  }

  printBoxOffice(){
    this.statisticService.generateStatisticsReport().subscribe( res => {
      let blob: Blob = res.body as Blob;
      let url = window.URL.createObjectURL(blob);
      window.open(url);
    });
  }

  downloadBoxOffice(){
    const today = new Date().toISOString().slice(0, 10);
    this.statisticService.generateStatisticsReport().subscribe( res => {
      let blob: Blob = res.body as Blob;
      let url = window.URL.createObjectURL(blob);
      let anchor = document.createElement('a');
      anchor.download = 'Report_RecordClique_' + today;
      anchor.href = url;
      anchor.click();
    });
  }

}
