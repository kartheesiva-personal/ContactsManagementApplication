import { Component } from '@angular/core';
import { ContactService } from '../Service/contact.service';
import { Contact } from '../Modal/Contact';


@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrl: 'app.component.css'
})
export class AppComponent {
  contacts: Contact[] = [];
  
  constructor(private contactService: ContactService) { }

  ngOnInit() {
    this.reloadContacts();
  }

  reloadContacts(): void {
    // Fetch the updated list of contacts from the API
    this.contactService.getContacts().subscribe((data) => {
      this.contacts = data;
    });
  }

  title = 'contactsmanagementapplication.client';
}
