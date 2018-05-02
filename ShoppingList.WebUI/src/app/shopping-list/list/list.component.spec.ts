import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { ListComponent } from './list.component';
import { Observable } from 'rxjs/Observable';
import { Item } from '../models/item';
import { of } from 'rxjs/observable/of';
import { ListService } from '../services/list.service';

class MockListService {

  getAll(): Observable<Item[]> {
    return of([]);
  }

  add(name: string): Observable<Item> {
    return of(null);
  }

  remove(id: number): Observable<boolean> {
    return of(false);
  }
}

describe('ListComponent', () => {
  let component: ListComponent;
  let fixture: ComponentFixture<ListComponent>;
  let listService: MockListService;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListComponent ],
      providers: [
        {provide: ListService, useClass: MockListService}
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListComponent);
    component = fixture.componentInstance;
    listService = TestBed.get(ListService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('removes item from list when item deleted', fakeAsync(() => {
    // arrange
    const removeSpy = spyOn(listService, 'remove').and.returnValue(of(true));
    const getAllSpy = spyOn(listService, 'getAll').and.returnValue(of([{ id: 1, name: "test1" }, { id: 2, name: "test2" }]));
    
    component.ngOnInit();
    tick(1);
    const initialItemsLength = component.items.length;

    // act
    component.remove(1);
    tick(2);

    // assert
    expect(initialItemsLength).toBe(2);
    expect(component.items.length).toBe(1);
    expect(removeSpy).toHaveBeenCalled();
    expect(getAllSpy).toHaveBeenCalled();
  }))
});
