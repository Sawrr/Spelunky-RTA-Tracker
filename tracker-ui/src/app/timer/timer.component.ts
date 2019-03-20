import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { Router } from '@angular/router';
import { interval } from 'rxjs';
import { sprintf } from 'sprintf-js';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})
export class TimerComponent implements OnInit {

  constructor(private http: HttpService, private router: Router) { }

  time: string = "0.00";
  endTime: number = 0;
  startTime: number = 0;

  ngOnInit() {
    interval(30).subscribe(() => {
      this.updateTimer();
    });
  }

  checkForStartAndEnd(res) {
    if (this.startTime == 0) {
      this.startTime = res.startTime;
    }
    if (res.data.journalTime > 0) {
      this.endTime = res.data.journalTime;
      this.formatTimer(this.endTime);
    }
  }

  updateTimer() {
    if (this.startTime == 0 || this.endTime > 0) return;

    let time = + new Date();
    this.formatTimer(time);
  }

  formatTimer(time: number) {
    time -= this.startTime;

    if (time < 60 * 1000) {
        // time < 1 minute
        this.time = sprintf('%1$d.%2$02d', time / 1000, (time % 1000) / 10);
    } else if (time < 60 * 60 * 1000) {
        // time < 1 hour
        this.time = sprintf('%1$d:%2$02d.%3$02d', time / 60000, (time % 60000) / 1000, (time % 1000) / 10);
    } else {
        // other
        this.time = sprintf('%1$d:%2$02d:%3$02d.%4$02d', time / 3600000, (time % 3600000) / 60000, (time % 60000) / 1000, (time % 1000) / 10);
    }
  }

}
