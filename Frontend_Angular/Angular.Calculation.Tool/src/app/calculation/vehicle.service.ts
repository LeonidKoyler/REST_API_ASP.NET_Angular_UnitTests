import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';

import { Vehicle } from './vehicle.model';

@Injectable({ providedIn: 'root' })
export class VehicleService {
  url: string = 'https://localhost:7198/api/vehicle';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private httpClient: HttpClient) {}

  calculate(vehicle: Vehicle): Observable<any> {
    return this.httpClient
      .post(this.url, JSON.stringify(vehicle), this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: any) {
    let errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    return throwError(() => new Error(errorMessage));
  }
}
