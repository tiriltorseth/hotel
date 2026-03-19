# Hotelsystem - C#
**Arbeidskrav 2, OOP**

### Functionality
Consolebased hotelsystem written in C#.
The system handles guests, rooms and a simulation of 
payment using an interface using a local menu.

Main functionalities are: 
1. Show available rooms
2. Create booking
3. Check in
4. Check out
5. Show my bookings
6. Register new guest
7. Cancel Bookings
0. Exit


### Structure
Indentations represent inheritence 

- Room
    - SingleRoom
    - DoubleRoom
    - Suite
- Guest
    - RegularGuest
    - VipGuest
- Ipayable
    - CardPayment
    - VippsPayment
- Booking
- Hotel
- Program.cs

### Execution
Run the program from Program.cs. A local menu will appear in the terminal.
The user chooses between the numbers 1-7.
Each choice has demands for input, such as phonenumber in ## ## ## ## or id's in G/BK###. If not followed, the program will restart 
and write an error message. Choose 0 to exit program.

### Use of AI
To view all AI used in the assignment, see file BrukAvAI.md

### Reflekstions
(Edited by ChatGPT to ensure proper flow and wording)

This assignment felt more manageable than the first, as I had a better understanding of object-oriented principles and underlying logic. While I still find problem-solving challenging and sometimes get stuck in my own mistakes, I notice clear improvement in how I approach tasks and work more independently.

The most difficult parts were the CreateBooking and GetAvailableRooms logic, especially checking room availability within a given time period. After several days of trial and error, I relied on AI for support. I also found interfaces challenging at first, but it became clearer once I understood that the payment system was only a simulation.

Despite these challenges, I have learned a lot. Object-oriented programming still does not feel fully intuitive, but I required significantly less guidance compared to the first assignment and solved more on my own.

The tests were especially valuable in identifying issues in my code. They gave me a sense of accomplishment when I was able to debug and resolve problems independently. I struggled with Test 3 due to a larger logical error in CreateBooking, but the remaining tests were both useful and educational, as they encouraged reflection on my own implementation.

Overall, the assignment was appropriately challenging and very educational. It has strengthened my understanding of C#, object-oriented design, and my own problem-solving process.

