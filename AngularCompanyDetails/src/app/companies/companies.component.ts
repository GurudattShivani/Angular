import { Component, OnInit } from '@angular/core';
import { CompanyService } from './shared/company.service';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',  
  providers: [CompanyService]
})
export class CompaniesComponent implements OnInit {

  constructor(private companyService:CompanyService) { }

  ngOnInit() {
  }

}
