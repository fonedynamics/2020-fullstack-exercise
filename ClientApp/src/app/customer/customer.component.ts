import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent {
  public customers: Customer[];
  filteredCustomers: Customer[];
  sortByForCustomers?: string;
  tags?: string[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authService: AuthService, router: Router) {
    if (!authService.isLoggedIn()) {
      router.navigate(["/login"]);
    }
    http.get<Customer[]>(baseUrl + 'api/Customer').subscribe(result => {
      this.customers = result;
      this.filteredCustomers = this.customers;
      const tags: string[] = [];
      this.tags = [];
      if (this.customers) {
        for (const customer of this.customers) {
          for (const tag of customer.tags) {
            tags.push(tag);
          }
        }
      }
      this.tags = tags.reduce((unique, item) => unique.includes(item) ? unique : [...unique, item], []);
    }, error => console.error(error));
  }

  filterOnTags(tag?: string) {
    this.filteredCustomers = [];
    if (!tag || tag === "") {
      this.filteredCustomers = this.customers;
    };

    for (const customer of this.customers) {
      if (customer.tags.includes(tag)) {
        this.filteredCustomers.push(customer);
      }
    }

    return this.filteredCustomers;
  }

  filterOnNumEmployees(numEmployees?: string) {
    this.filteredCustomers = [];
    if (!numEmployees || numEmployees === "") {
      this.filteredCustomers = this.customers;
    };
    const customersWith1To10Employees: Customer[] = [];
    const customersWith11To50Employees: Customer[] = [];
    const customersWithMoreThan50Employees: Customer[] = [];

    for (const customer of this.customers) {
      if (customer.numEmployees >= 1 && customer.numEmployees <= 10) {
        customersWith1To10Employees.push(customer);
        } else if (customer.numEmployees >= 11 && customer.numEmployees <= 50) {
        customersWith11To50Employees.push(customer);
        } else {
        customersWithMoreThan50Employees.push(customer);
        }
    }
    this.filteredCustomers = parseInt(numEmployees) === 1
      ? customersWith1To10Employees
      : parseInt(numEmployees) === 11
      ? customersWith11To50Employees
      : customersWithMoreThan50Employees;
    return this.filteredCustomers;
  }

  // sorts the customers
  sortCustomers(sortBy: string) {

    if (this.sortByForCustomers && this.sortByForCustomers === sortBy) {
      if (sortBy === "id") {
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.id && n2.id) && (n1.id > n2.id)) {
            return -1;
          }
          if ((n1.id && n2.id) && (n1.id < n2.id)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "name") {
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.name && n2.name) && (n1.name > n2.name)) {
            return -1;
          }
          if ((n1.name && n2.name) && (n1.name < n2.name)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "numEmployees") {
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees > n2.numEmployees)) {
            return -1;
          }
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees < n2.numEmployees)) {
            return 1;
          }
          return 0;
        });
      } else if (sortBy === "tags") {
        this.filteredCustomers.sort((n1, n2) => {
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
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.id && n2.id) && (n1.id > n2.id)) {
            return 1;
          }
          if ((n1.id && n2.id) && (n1.id < n2.id)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "name") {
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.name && n2.name) && (n1.name > n2.name)) {
            return 1;
          }
          if ((n1.name && n2.name) && (n1.name < n2.name)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "numEmployees") {
        this.filteredCustomers.sort((n1, n2) => {
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees > n2.numEmployees)) {
            return 1;
          }
          if ((n1.numEmployees && n2.numEmployees) && (n1.numEmployees < n2.numEmployees)) {
            return -1;
          }
          return 0;
        });
      } else if (sortBy === "tags") {
        this.filteredCustomers.sort((n1, n2) => {
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
