import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-account-creation',
  templateUrl: './account-creation.component.html'
})
export class AccountCreationComponent implements OnInit {
  accountCreationForm: FormGroup;
  http: HttpClient;
  baseUrl: any;
  headers: HttpHeaders;
  clientId = 0;
  interestFreq = [];
  accountType = [];
  isShowFreq = true;
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
    this.getInterestRates();
    this.accountCreationForm = this._fb.group({
      accountType: ['', Validators.required],
      interestFreq: ['', Validators.required]
    });

  }

  createBankAccount() {
    let param = {
      clientId: this.clientId,
      bankId: 1,
      annualInterestId: this.accountCreationForm.get('accountType').value!= null? parseInt(this.accountCreationForm.get('accountType').value): 0,
      interestPayingFrequency: this.accountCreationForm.get('interestFreq').value != "" ? parseInt(this.accountCreationForm.get('interestFreq').value): 0 
    }
    this.http.post<boolean>(this.baseUrl + 'account/AddClientAccount', param, { headers: this.headers }).subscribe(result => {
      this.router.navigate(['/manage-account/' + this.clientId]);
    }, error => alert(error.error) );
  }

  getInterestRates() {
    this.http.get<any>(this.baseUrl + 'account/GetInterestRatesAndPeriod', { headers: this.headers }).subscribe(result => {
      if (result) {
        this.accountType = result.accountSettings;
        this.accountType.forEach(acc => {
          acc.description = (acc.accountType == AccountType.SavingsAccount ? "Savings Account" : "Deposit Account") + ' - ';
          acc.description += acc.accountType == AccountType.DepositAccount ? acc.depositPeriodInDays + ' days Period - ' : 'N/A - ';
          acc.description +=  acc.annualInterestRate + '% Annual Interest'; 
        });
        this.interestFreq = result.depositFrequency;
      }
    }, error => console.error(error));
  }

  onAccountTypeChange() {
    let accountObj = this.accountType.find(x => x.id == this.accountCreationForm.get('accountType').value);
    if (accountObj.accountType == AccountType.SavingsAccount) {
      this.isShowFreq = false;
    } else {
      this.isShowFreq = true;
    }
  }
}

export enum AccountType {
  SavingsAccount = 1,
  DepositAccount = 2
} 

