import { BrowserRouter, Router } from 'react-router-dom';
import history from './Utils/history';
import Routes from './Routes/Routes';
import Navbar from './components/Navigation/Navbar';
import { SnackbarProvider } from 'notistack';
import { useState } from 'react';
import { currentUserValue } from './Utils/authenticationService';

function App() {
  const [currentUser, setCurrentUser] = useState(currentUserValue());
  return (
    <SnackbarProvider maxSnack={3}>
      <Router history={history}>
        <BrowserRouter>
          <Navbar currentUser={currentUser} setCurrentUser={setCurrentUser} />
          <Routes setCurrentUser={setCurrentUser} />
        </BrowserRouter>
      </Router>
    </SnackbarProvider>
  );
}

export default App;
