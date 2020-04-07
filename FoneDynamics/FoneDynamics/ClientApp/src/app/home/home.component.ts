import { Component } from '@angular/core';
import { CustomerService } from '../_services/customer.service';
import { Customer } from '../_models/customer';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    public customers: Customer[];
    public searchkey: string = "";
    public numberOfEmployeeFilters: number[];
    public svc: CustomerService;
    public selectedNumberOfEmployeeFilter: number = 0;


    public itemsToDisplay: number = 5;
    public totalItems: number = 0;
    public pageIndex: number = 0;
    public numberOfPages: number = 0;

  public sortCol: string = "";
  public sortAsc: boolean = true;

    timer: any;

    constructor(private customerService: CustomerService) {
        this.svc = customerService;
        this.search(this);
    }

    textChange(event):void {
        this.searchkey = event.target.value;
        clearTimeout(this.timer);
        this.timer = setTimeout(() => this.search(this), 1000);
    }

    search(thisRef): void {
        thisRef.customers = [];
        thisRef.svc.fetch(this.sortCol, this.sortAsc, this.selectedNumberOfEmployeeFilter, thisRef.searchkey, this.pageIndex * this.itemsToDisplay, this.itemsToDisplay).subscribe(c => {
            thisRef.customers = c.results;
            thisRef.numberOfEmployeeFilters = c.numberOfEmployeeFilters;
            thisRef.totalItems = c.totalItems;
            thisRef.numberOfPages = Math.ceil(c.totalItems / this.itemsToDisplay);

        });
    }

    filterChange(event): void {
        debugger;
        this.selectedNumberOfEmployeeFilter = event.target.value;
        this.search(this);
    }
    goToPage(page) {
        debugger;
        this.pageIndex = page;
        this.search(this);
    }
  sort(colName): void {
    debugger;
    if (this.sortCol == colName) {
      this.sortAsc = !this.sortAsc;
    }
    this.sortCol = colName;

    this.search(this);
  }
}
