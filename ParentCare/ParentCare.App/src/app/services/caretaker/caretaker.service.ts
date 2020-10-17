import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Caretaker } from 'src/model/Caretaker';



@Injectable({
	providedIn: 'root'
})
export class CaretakerService {
	url = environment.apiUrl + 'Caretaker';
	constructor(private http: HttpClient) { }

	post(caretaker: Caretaker): Observable<any> {
		return this.http.post(`${this.url}`, caretaker);
	}
}
