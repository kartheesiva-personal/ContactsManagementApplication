import { Component, Input, Output, AfterViewInit, ViewChild, ElementRef, EventEmitter } from '@angular/core';
import { ContactService } from '../../Service/contact.service';
import { Contact } from '../../Modal/Contact';
@Component({
  selector: 'app-contact-list',
  templateUrl: 'contact-list.component.html',
  styleUrls: ['contact-list.component.scss']
})
export class ContactListComponent implements AfterViewInit {
  @ViewChild('contactFormModal') modal!: ElementRef;
  @Input() contacts: any[] = [];
  @Output() deleteContact = new EventEmitter<number>();
  @Output() editContact = new EventEmitter<any>();

  ngAfterViewInit() {
    // Wait until view is initialized and modal is available in the DOM
    const modalElement = this.modal.nativeElement;
    const modal = new window.bootstrap.Modal(modalElement);
  }
  // Inject the service in the constructor
  constructor(private contactService: ContactService) { }

  ngOnInit(): void {
    this.contactService.getContacts().subscribe((data: Contact[]) => {
      this.contacts = data;
    });
  }

  loadContacts(): void {
    this.contactService.getContacts().subscribe((data: Contact[]) => {
      this.contacts = data;
    });
  }

  openContactFormModal(): void {
    if (this.modal) {
      const modalElement = this.modal.nativeElement;
      const modal = new window.bootstrap.Modal(modalElement);
      modal.show(); // Open the modal
    }
  }

  closeContactFormModal(): void {
    if (this.modal) {
      const modalElement = this.modal.nativeElement;
      const modal = new window.bootstrap.Modal(modalElement);
      modal.hide(); // Close the modal
    }
  }

  reloadContacts(): void {
    this.loadContacts();
    this.closeContactFormModal();  // Close the modal after saving the contact
  }
  // Delete contact
  onDelete(contactId: number) {
    const modalElement = this.modal.nativeElement;
    const modal = new window.bootstrap.Modal(modalElement);
    modal.show(); // Open the modal
    this.deleteContact.emit(contactId);
  }
  //deleteContact(id: number): void {
  //  if (confirm('Are you sure you want to delete this contact?')) {
  //    this.contactService.deleteContact(id).subscribe(() => {
  //      this.contacts = this.contacts.filter(c => c.id !== id);
  //    });
  //  }
  //}

  // Edit contact
  onEdit(contact: any) {
    this.editContact.emit(contact);
  }

  addNewContact(): void {
    // Clear the form or navigate to the contact form for adding a new contact
    console.log('Adding new contact');
  }
}
