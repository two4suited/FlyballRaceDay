import React from 'react';
import logo from './logo.svg';
import { BrowserRouter as Router, Route,Link, Routes} from 'react-router-dom'
import './App.css';
import TournamentList from './Tournament/TournamentList';
import TournamentForm from './Tournament/TournamentForm';

function AppBackup() {
  return (
    <div className="App">
      <header className="App-header">       
        <div>    
      <Router>
            <div>         
                <Link to="/create-tournament">Create Tournament</Link>
                <Link to="/list-tournament">View Tournaments</Link>                
                <Routes>
                  <Route path="/create-tournament" element={<TournamentForm/>} />
                  <Route path="/list-tournament" element={<TournamentList/>} />
                </Routes>
            </div>
        </Router>       
    </div>
      </header>
      
    </div>

    
  );
}

export default AppBackup;
