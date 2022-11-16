# Finance Manager Idea Document

A program to manage financial investments.

Skip to section:

1. [Problem](#problem-definition)
2. [Requirements](#requirements)
    - [API](#api)
    - [Store Investments](#store-investments)
    - [Date Based Predictions](#date-based-predictions)
3. [Specifications](#specifications)
    - [Main Screen](#main-screen)
    - [Stock Search](#stock-search-window-or-tab)

## Problem Definition

The user must be able to track financial investments in real time using current data.

This is an app that will allow the client to not only track current investment instruments, but also play “what if” games regarding potential investments. The client envisions a system that will track investment values (eg, stock prices) and report those. The client will have a number of investments in the portfolio. The app will track and report the total value of the portfolio. This app will rely on the use of an external financial API for “real time” stock data.

## Requirements

### _API_

The program must be able to get current stock data from a database of stock data. It will use [Yahoo Finance API](https://query1.finance.yahoo.com/v7/finance/). Most major endpoints are described and demonstrated on [SyncWith.com](https://syncwith.com/yahoo-finance/yahoo-finance-api). The most important pieces of data will be the current stock price and the current symbol.

### _Store Investments_

The program will store current investments to disk using the time of purchase and stock symbol. It will be able to load these in the future for calculation to see the changes in the stock values since time of purchase. The price the stock was bought at will also be stored to prevent multiple calls to the API on startup every time. This feature will also be able to access historical stock data to add stocks to the portfolio that were purchased before the use of this program.

### _Database_

The program will store user info and stock info a SQLite type local database. This allows for easy updating and quering of all data needed. This will use an Entity Framework to convert data from classes to SQL domains. This will allow the program to store various types of data, including the posibility for multiple users in the future.

### _Date Based Predictions_

Using historical stock data, the user can give the software a stock symbol and date to see the change in price if they had bought the stock at a given time.

## Specifications

The main screen will basically display everything stored in the file and the stock search will display data from the API. The main screen/file is updated on start each time

### _Main Screen_

The main screen will have:

-   all current investments
    -   symbol
    -   price
    -   percent change from the previous close
-   portfolio info
    -   current value of the portfolio
    -   percent change of the portfolio from a given date (default date will be 24-hours before current time)
-   [Stock Search] button
    -   opens stock search window

### _Stock Search Window (or Tab)_

The Stock Search window will act as the way to store investments and see historical data. The window will have:

-   input for stock lookup
    -   search bar for stock for current stock info (displays price, percent change from previous close)
    -   date input to change date for stock lookup (displays price, percent change from current date)
-   input for stock amount
    -   input for the amount of shares to use in the total price as well as to add to portfolio
-   Total Price outputs the selected stock price \* stock amount
-   input for manual override
    -   manually select price that shares were bought at to add to portfolio
-   [Add Stock] button
    -   will add current stock (at selected date and quantity) to stock list/file/portfolio
    -   display success message
    -   update main screen
