import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CustomerService } from '../services/customer.service';
import { CustomerModel } from '../models/customer.model';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {

  public types: string[] = ['Large', 'Small'];
  public customers: CustomerModel[] = [];

  public newCustomer: CustomerModel = {
    customerId: null,
    name: null,
    type: undefined
  };

  constructor(
    private customerService: CustomerService) { }

  ngOnInit() {
    this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
  }

  public createCustomer(form: NgForm): void {
    if (form.invalid) {
      alert('form is not valid');
    } else {
      this.customerService.CreateJob(this.newCustomer).then(() => {
        this.customerService.GetCustomers().subscribe(customers => this.customers = customers);
      });
    }
  }

}
