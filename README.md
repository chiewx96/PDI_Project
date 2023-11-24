# PDI_Project
- This is part of a warehouse tracking/management system that will keep track of details of products being entered and shipped out.
- When production finished packing a bag of product, the bag will be put on top of the weighting machine.
- Once the stable weight is captured (3 seconds), system will store the weight of the bag with auto generated fixed formatting reference number into database.
- System will prints out label for operator to stick onto the bag.
- Only those bags with label allow to enter warehouse for storage.
- For products that need to be ship to customers, operator have to use handheld to scan qr code on each label stick on top of the bag, and these data will be updated to database.
- The officer then able to view how many bags with weight and inbound date & time.
- The officer will get to filter those products and generate delivery order / warehouse reports.

## Required software
1. Mysql
2. NodeJs
3. Bartender printing service

## Desktop Application
- C# .NET 6
- This will be installed on warehouse computer and office computer.
- Warehouse computer : connects weighting machine and prints label.
- Office computer : printing delivery order & filter reports.

### General function
1. Module accessible based on access level of current logged in users.
2. User can be created and modified with different access levels, username & password.

### Weighting function
1. Reads weight from weighting device with TCPIP.
2. Storing of reading with scanned reference number into database.
3. Details will be send to console application to print label.
4. Once label has been printed, the product will be in 'Inbound' status.
5. Can issue reprint signal to console application to reprint the desired label.

## Printer & Logging console application
- C# .NET Framework 3.5 
- Connects to desktop application through TCPIP.
- Separated from desktop application due to bartender dll does not support latest framework.

### Print function
1. Connects to bartender printer.
2. Print label using template stored and data passed from desktop application.

### Logging function
1. Logging of data locally with NLog extension and separated with date. 

## Handheld PWA
1. VueJs (Vue 3).
2. Fetch data from handheld API using Fetch API.
3. Implements login function with token and handling using Vuex state management.
4. Scanning QR : able to scan QR code on label with using device camera or IR scanner.
5. On confirmation of scanned qr codes will update scanned items into 'Outbound' status.

## Handheld API
1. Using C# .NET 6 framework.
2. Entity Framework Core, code first approach.
3. Responsible for migration and handling request from handheld web application.
