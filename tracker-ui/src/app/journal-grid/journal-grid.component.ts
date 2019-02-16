import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { interval } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-journal-grid',
  templateUrl: './journal-grid.component.html',
  styleUrls: ['./journal-grid.component.css']
})
export class JournalGridComponent implements OnInit {

  constructor(private http: HttpService, private router: Router) { }

  ngOnInit() {
    this.monsters = [];
    this.items = [];
    this.traps = [];
    
    for (let i = 0; i < 56; i++) {
      this.monsters.push(`assets/monsters/Mon_${i}.png`);
    }
    for (let i = 0; i < 34; i++) {
      this.items.push(`assets/items/Item_${i}.png`);
    }
    for (let i = 0; i < 14; i++) {
      this.traps.push(`assets/traps/Trap_${i}.png`);
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
          this.monsters[i] = '';
        }
      }

      // Items
      for (let i = 0; i < 34; i++) {
        if (res.data.journal.items[i]) {
          this.items[i] = '';
        }
      }

      // Traps
      for (let i = 0; i < 14; i++) {
        if (res.data.journal.traps[i]) {
          this.traps[i] = '';
        }
      }
    });
  }

  private roomCode: string;

  private monsters: string[];
  private items: string[];
  private traps: string[];

}
