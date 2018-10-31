import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../shared/company.service';
import { Company } from '../shared/company.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html'
})
export class CompanyListComponent implements OnInit {

  constructor(private companyService: CompanyService, private toastr: ToastrService) { }

  //To get company details()
  ngOnInit() {
    this.companyService.getCompanyList();
  }

  //For edit
  showForEdit(comp : Company)
  {
    this.companyService.selectedCompany = Object.assign({}, comp);
  }

  onDelete(id: number){
    if(confirm('Are you sure to delete this record?') == true){
      this.companyService.deleteCompany(id)
      .subscribe(x => {
        this.companyService.getCompanyList();
        this.toastr.warning("Deleted Successfully", "Company Details")
      })
    }    
  }
}
