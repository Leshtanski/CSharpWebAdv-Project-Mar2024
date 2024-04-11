1. Setting up Seller

After creating the Database, please, set up the Seller by creating a registered User and then let him Become Seller.
In the database add the newly created Seller's SellerId into the SellerId column of the first three seeded products.

2. Setting up Admin

Register a new user with the Admin email address in Common.GeneralApplicationConstants (admin@admin.bg).
Then make him Become Seller. After that, please, go to program.cs and uncomment the lines 83-86 and rebuild the app.