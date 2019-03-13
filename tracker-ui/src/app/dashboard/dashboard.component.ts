import { Component, OnInit, ViewChild } from '@angular/core';
import { TimerComponent } from '../timer/timer.component';
import { JournalGridComponent } from '../journal-grid/journal-grid.component';
import { HttpService } from '../http.service';
import { Router } from '@angular/router';
import { interval } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private http: HttpService, private router: Router) { }

  roomCode: string;

  ngOnInit() {
    this.roomCode = this.router.url.replace('/', '');

    interval(2500).subscribe(() => {
      this.http.getStatus(this.roomCode).subscribe((res: any) => {
        this.timerComponent.checkForStartAndEnd(res);
        this.journalGridComponent.updateData(res);
      });
    });
  }

  @ViewChild(TimerComponent)
  private timerComponent: TimerComponent;

  @ViewChild(JournalGridComponent)
  private journalGridComponent: JournalGridComponent;
}
