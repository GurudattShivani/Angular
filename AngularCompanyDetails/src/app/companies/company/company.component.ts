import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../shared/company.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
//import { FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html' 
})
export class CompanyComponent implements OnInit { 
  
  constructor(private companyService: CompanyService, private toastr: ToastrService) { }

  //Call the reset button when the component is loaded
  ngOnInit() {
    this.resetForm();
  }

  //Implement the reset button functionality
  resetForm(form? : NgForm)
  {
    if (form != null)
    form.reset();
    
    this.companyService.selectedCompany = {
      CompanyId : null,
      CompanyName : '',
      LastName : '',
      Email : '',
      PhoneNumber : null,
    }
  }

  onSubmit(form : NgForm)
  {     
    
    //Consume the WebAPI "Post" method
    //When CompanyId is null, Insert new values, else Update
    if(form.value.CompanyId == null){
        this.companyService.postCompany(form.value)
      .subscribe(data=>
      {
        //If company email and phone number exists, throw error
        if (data && (data.CompanyName == null && (data.Email == null || data.LastName == null))) {         
          if(data.Email == null || data.PhoneNumber == 0){
            this.toastr.error("PhoneNo/Email exists-Duplicate values not allowed!", 'Company Details');
          }
          this.resetForm(form);
          return;
        }        

        this.resetForm(form);
        //Update the company values in the displayed form 
        this.companyService.getCompanyList();
        this.toastr.success('Details Added Successfully', 'Company Details');
      })
    } 
    else
    {
      //Consume the WebAPI "Put" method
      this.companyService.putCompany(form.value.CompanyId, form.value)
      .subscribe(data=>
      {
        this.resetForm(form);
        //Update the company values in the displayed form 
        this.companyService.getCompanyList();
        this.toastr.success('Details Updated Successfully', 'Company Details');
      })
    }
    
  }
}
