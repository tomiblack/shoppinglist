import { Component, OnInit } from '@angular/core';
import { Item } from '../models/item';
import { ListService } from '../services/list.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-shopping-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  items: Item[]

  constructor(private listService: ListService) { }

  ngOnInit() {
    this.listService.getAll().subscribe(data => this.items = data);
  }

  remove(id: number) {
    this.listService.remove(id).subscribe(result => {
      if (result) {
        this.items = this.items.filter(x => x.id === id);
      }
    })
  }

  add(name: string) {
    this.listService.add(name).subscribe(result => {
      if (result) {
        this.items.push(result);
      }
    })
  }
}
