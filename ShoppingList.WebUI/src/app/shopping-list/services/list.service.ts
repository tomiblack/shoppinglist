import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Item } from "../models/item";
import { HttpClient } from "@angular/common/http";
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';

@Injectable()
export class ListService {

    constructor(private httpClient: HttpClient) { }

    getAll(): Observable<Item[]> {
        return this.httpClient.get<Item[]>('/api/v1/items').pipe(
            catchError(err => {
                // TODO: error notification
                console.error(err);
                return of([]);
            })
        );
    }

    add(name: string): Observable<Item> {
        return this.httpClient.post<Item>('/api/v1/items', { name }).pipe(
            catchError(err => {
                // TODO: error notification
                console.error(err);
                return of(null);
            })
        )
    }

    remove(id: number): Observable<boolean> {
        return this.httpClient.delete<void>(`/api/v1/items/${id}`).pipe(
            map(() => true),
            catchError(err => {
                // TODO: error notification
                console.error(err);
                return of<boolean>(false);
            })
        );
    }
}