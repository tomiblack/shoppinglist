import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list/list.component';
import { ListService } from './services/list.service';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ListComponent
  ],
  declarations: [ListComponent],
  providers: [
    ListService
  ]
})
export class ShoppingListModule { }
