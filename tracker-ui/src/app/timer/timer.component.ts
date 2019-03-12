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

  time: string;
  startTime: number = 0;

  public roomCode: string;

  ngOnInit() {
    this.roomCode = this.router.url.replace('/', '');

    interval(10).subscribe(() => {
      this.updateTimer();
    });

    interval(100).subscribe(() => {
      if (this.startTime === 0) {
        this.checkForStart();
      }
    });
  }

  checkForStart() {
    // this.http.getStatus(this.roomCode).subscribe((res: any) => {
    //   this.startTime = res.startTime;
    // });
    this.startTime = + new Date();
  }

  updateTimer() {
    let time = + new Date() - this.startTime;
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
