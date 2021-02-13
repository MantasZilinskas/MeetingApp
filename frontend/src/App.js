import React from 'react';
import { BrowserRouter, Router } from 'react-router-dom';
import history from './Utils/history';
import Routes from './Routes/Routes';
import Navbar from './components/Navigation/Navbar';
import { SnackbarProvider } from 'notistack';
import { useState } from 'react';
import { currentUserValue } from './Utils/authenticationService';
import StickyFooter from './components/Footer';

function App() {
  const [currentUser, setCurrentUser] = useState(currentUserValue());
  return (
    <SnackbarProvider maxSnack={3}>
      <Router history={history}>
        <BrowserRouter basename="/">
          <Navbar currentUser={currentUser} setCurrentUser={setCurrentUser} />
          <Routes setCurrentUser={setCurrentUser} />
          <StickyFooter />
        </BrowserRouter>
      </Router>
    </SnackbarProvider>
  );
}

export default App;
