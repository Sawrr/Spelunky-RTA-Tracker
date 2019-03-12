import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { interval } from 'rxjs';
import { Router } from '@angular/router';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-journal-grid',
  templateUrl: './journal-grid.component.html',
  styleUrls: ['./journal-grid.component.css'],
  animations: [
    trigger('unlock', [
      state('unlocked', style({
        opacity: 1
      })),
      state('locked', style({
        opacity: 0
      })),
      transition('* => *', [
        animate('1500ms ease-out')
      ])
    ]
  )]
})
export class JournalGridComponent implements OnInit {

  constructor(private http: HttpService, private router: Router) { }

  ngOnInit() {
    this.monsters = [];
    this.items = [];
    this.traps = [];
    
    for (let i = 0; i < 56; i++) {
      this.monsters.push({
        src: `assets/monsters/Mon_${i}.png`,
        unlocked: false
      });
    }
    for (let i = 0; i < 34; i++) {
      this.items.push({
        src: `assets/items/Item_${i}.png`,
        unlocked: false
      });
    }
    for (let i = 0; i < 14; i++) {
      this.traps.push({
        src: `assets/traps/Trap_${i}.png`,
        unlocked: false
      });
    }

    this.roomCode = this.router.url.replace('/', '');

    console.log(this.roomCode);

    interval(5000).subscribe(() => {
      this.updateData();
    });

    this.updateData();
  }

  private updateData() {
    console.log(this.roomCode);
    this.http.getStatus(this.roomCode).subscribe((res: any) => {      
      // Monsters
      for (let i = 0; i < 56; i++) {
        if (res.data.journal.monsters[i]) {
          this.monsters[i].unlocked = true;
        }
      }

      // Items
      for (let i = 0; i < 34; i++) {
        if (res.data.journal.items[i]) {
          this.items[i].unlocked = true;
        }
      }

      // Traps
      for (let i = 0; i < 14; i++) {
        if (res.data.journal.traps[i]) {
          this.traps[i].unlocked = true;
        }
      }
    });
  }

  public roomCode: string;

  public monsters: {src: string, unlocked: boolean}[];
  public items:  {src: string, unlocked: boolean}[];
  public traps:  {src: string, unlocked: boolean}[];

}
