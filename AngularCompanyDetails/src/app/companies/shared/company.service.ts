import { Injectable } from '@angular/core';
import { Company } from './company.model';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  selectedCompany : Company;
  companyList : Company[];

  constructor(private http: Http) { }

  //Define a function to get the "Company" list.
  //Convert Observable<Response> to "Company" array
  getCompanyList(){
    this.http.get('http://localhost:49559/api/Companies')
    .pipe(map((data : Response) => {
      return data.json() as Company[];
    })).toPromise().then(x => {
      this.companyList = x;
    })
  }

  /*
  getCompanyList(id: comp) {  
    var companyObj = JSON.stringify(comp);
    var headerOptions = new Headers({'Content-Type' : 'application/json'})
    let options = new RequestOptions({ headers: headers });
    return this.http.get('http://localhost:49221/api/Employee/GetEdit/' + id, companyObj, headerOptions)
        .map((res: Response) => res.json());    
  }
  */
 
  //Define a function to call the "PostCompany" method - Insert company details
  postCompany(comp : Company){
    var companyObj = JSON.stringify(comp);
    var headerOptions = new Headers({'Content-Type' : 'application/json'})
    var requestOptions = new RequestOptions({method : RequestMethod.Post, headers : headerOptions});
    //Convert Observable<Response> to Observable<Json>
    return this.http.post('http://localhost:49559/api/Companies', companyObj, requestOptions)
    .pipe(map(x => x.json()));    
  } 
  
  //Define a function to call the "PutCompany" method - Update company details
  putCompany(id, comp){
    var companyObj = JSON.stringify(comp);
    var headerOptions = new Headers({'Content-Type' : 'application/json'})
    var requestOptions = new RequestOptions({method : RequestMethod.Put, headers : headerOptions});
    //Convert Observable<Response> to Observable<Json>
    return this.http.post('http://localhost:49559/api/Companies/' + id, companyObj, requestOptions)
    .pipe(map(x => x.json()));    
  }  

  //Define a function to delete company details and convert Observable<Response> to Observable<Json>
  deleteCompany(id : number){
    return this.http.delete('http://localhost:49559/api/Companies/' + id).pipe(map(x => x.json()));
  }
}


