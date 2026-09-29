# Single Responsibility Principle (SRP)
## Task 1.1 - Identify Responsibilities

### 1. WardBoard
Responsibilities:
- Manage patient bed assignments.
- Calculate patient acuity scores.
- Generate and manage pager alerts.
- Generate nurse handoff notes.
- Export ward census data to CSV.

Why it violates SRP:
Bed management, medical scoring, alerting, report formatting,
and CSV export have different reasons to change.

### 2. CheckoutBasket
Responsibilities:
- Manage shopping basket items.
- Calculate subtotal and grand total.
- Process coupon discounts.
- Manage gift wrapping and gift messages.
- Handle payment authorization.

Why it violates SRP:
Cart management, pricing, coupon rules, gift packaging,
and payment authorization change independently.

### 3. SupportTicket
Responsibilities:
- Store ticket information and customer messages.
- Calculate ticket priority.
- Calculate SLA deadlines and breaches.
- Generate public replies and escalation messages.

Why it violates SRP:
Ticket storage, priority rules, SLA policy, and communication
templates have different reasons to change.

### 4. LoanDesk
Responsibilities:
- Store loan application information.
- Calculate risk scores and eligibility.
- Determine required compliance documents.
- Generate decision letters.
- Export underwriting data to CSV.

Why it violates SRP:
Risk calculations, compliance requirements, communication
templates, and export formats change independently.

### 5. CourseEnrollmentDesk
Responsibilities:
- Manage course registrations and capacity.
- Manage waitlists and student promotions.
- Generate welcome packets.
- Calculate and format tuition invoices.

Why it violates SRP:
Enrollment rules, waitlist policies, marketing content,
and financial calculations have separate reasons to change.

### 6. KitchenTicket
Responsibilities:
- Store kitchen order items.
- Detect allergens from ingredients.
- Calculate estimated preparation time.
- Determine kitchen expo lanes.
- Generate thermal printer tickets.

Why it violates SRP:
Order management, allergen regulations, preparation rules,
routing policies, and printing formats change independently.

### 7. SubscriptionBilling
Responsibilities:
- Calculate subscription proration.
- Generate invoice numbers.
- Track failed payments.
- Generate dunning emails.
- Export accounting ledger lines.

Why it violates SRP:
Pricing rules, invoice numbering, payment tracking,
customer communication, and accounting formats
have different reasons to change.

### 8. WarehousePickList
Responsibilities:
- Manage warehouse picking requirements.
- Allocate available stock.
- Calculate warehouse walking order.
- Generate picker instructions.
- Export XML batches for WMS integration.

Why it violates SRP:
Stock allocation, route planning, user instructions,
and external system integration change independently.

### 9. GradeBook
Responsibilities:
- Record student scores.
- Calculate average scores.
- Determine letter grades.
- Evaluate honor-roll eligibility.
- Generate transcripts and CSV exports.

Why it violates SRP:
Score storage, aggregation, grading policy,
academic recognition rules, and document formatting
have separate reasons to change.

### 10. AppointmentDesk
Responsibilities:
- Manage booked appointment slots.
- Validate clinic business hours.
- Search for available appointment slots.
- Generate ICS calendar events.
- Generate SMS reminders.

Why it violates SRP:
Booking logic, operating-hours policies,
calendar integration, and messaging content
change independently.