import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-manage-account',
  templateUrl: './manage-account.component.html'
})
export class ManageAccountComponent implements OnInit {
  accountCreationForm: FormGroup;
  http: HttpClient;
  baseUrl: any;
  headers: HttpHeaders;
  clientId = 0;
  accounts = [];
  AccountType = AccountType;
  constructor(
    http: HttpClient,
    private _fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    this.clientId = parseInt(this.route.snapshot.params.id);
  }

  ngOnInit() {
    this.getAccounts();
    this.accountCreationForm = this._fb.group({
      accountType: ['', Validators.required],
      interestFreq: ['', Validators.required]
    });

  }

  getAccounts() {
    this.http.get<any>(this.baseUrl + 'account/GetAccounts/?id=' + this.clientId, { headers: this.headers }).subscribe(result => {
      if (result) {
        this.accounts = result;
      }
    }, error => console.error(error));
  }

  WithdrawAmount(amount, accountId, item) {
    let param = {
      accountId: parseInt(accountId),
      amount: parseFloat(amount)
    }
    this.http.put<any>(this.baseUrl + 'account/WithdrawAmount', param, { headers: this.headers }).subscribe(result => {
      item.balance = result;
    }, error => { alert(error.error) });
  }

  DepositAmount(amount, accountId, item) {
    let param = {
      accountId: parseInt(accountId),
      amount: parseFloat(amount)
    }
    this.http.put<any>(this.baseUrl + 'account/DepositAmount', param, { headers: this.headers }).subscribe(result => {
      item.balance = result;
    }, error =>
      console.error(error));
  }

  OpenAccountClick() {
    this.router.navigate(['/account-creation/' + this.clientId]);
  }

}

export enum AccountType {
  SavingsAccount = 1,
  DepositAccount = 2
} 

