import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html'
})
export class CustomerComponent {
  public customers: Customer[];
  sortByForCustomers?: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Customer[]>(baseUrl + 'api/Customer').subscribe(result => {
      console.log('result', result);
      this.customers = result;
      console.log('this.customers', this.customers);
    }, error => console.error(error));
  }

  //TODO: Filters

  // sorts the customers
  sortCustomers(sortBy: string) {

    if (this.sortByForCustomers && this.sortByForCustomers === sortBy) {
      if (sortBy === "id") {
        this.customers.sort((n1, n2) => {
          if ((n1.id && n2.id) && (n1.id > n2.id)) {
            return -1;
          }
          if ((n1.id && n2.id) && (n1.id < n2.id)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "name") {
        this.customers.sort((n1, n2) => {
          if ((n1.name && n2.name) && (n1.name > n2.name)) {
            return -1;
          }
          if ((n1.name && n2.name) && (n1.name < n2.name)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "numEmployees") {
        this.customers.sort((n1, n2) => {
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees > n2.numEmployees)) {
            return -1;
          }
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees < n2.numEmployees)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "tags") {
        this.customers.sort((n1, n2) => {
          if ((n1.tags && n2.tags) && (n1.tags > n2.tags)) {
            return -1;
          }
          if ((n1.tags && n2.tags) && (n1.tags < n2.tags)) {
            return 1;
          }
          return 0;
        });
      }
      this.sortByForCustomers = "";
    } else {
      if (sortBy === "id") {
        this.customers.sort((n1, n2) => {
          if ((n1.id && n2.id) && (n1.id > n2.id)) {
            return 1;
          }
          if ((n1.id && n2.id) && (n1.id < n2.id)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "name") {
        this.customers.sort((n1, n2) => {
          if ((n1.name && n2.name) && (n1.name > n2.name)) {
            return 1;
          }
          if ((n1.name && n2.name) && (n1.name < n2.name)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "numEmployees") {
        this.customers.sort((n1, n2) => {
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees > n2.numEmployees)) {
            return 1;
          }
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees < n2.numEmployees)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "tags") {
        this.customers.sort((n1, n2) => {
          if ((n1.tags && n2.tags) && (n1.tags > n2.tags)) {
            return 1;
          }
          if ((n1.tags && n2.tags) && (n1.tags < n2.tags)) {
            return -1;
          }
          return 0;
        });
      }
      this.sortByForCustomers = sortBy;
    }


  }
}

interface Customer {
id: string;
numEmployees?: number;
name?: string;
tags?: string[];
}
