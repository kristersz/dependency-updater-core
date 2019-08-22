import React from 'react';
import logo from './logo.svg';
import './App.css';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Typography from '@material-ui/core/Typography';

function App() {

  async function buttonHandler() {
    await fetch('api/update/trigger');
  }

  return (
    <div className="App">
      <header className="App-header">
        <Typography variant="button" style={{ color: "black", marginBottom: 20 }}>
          Package auto updater
        </Typography>

        <TextField
          id="outlined-repository-input"
          label="Repository"
          variant="outlined"
        />

        <FormControlLabel
          control={<Checkbox color="primary" />}
          label="Include previews"
          style={{ color: "black" }}
        />
        <FormControlLabel
          control={<Checkbox color="primary" />}
          label="Update major versions"
          style={{ marginBottom: 30, color: "black" }}
        />

        <Button
          onClick={buttonHandler}
          variant="contained"
          color="secondary"
          className="ButtonSpin"
        >
          Trigger
        </Button>
      </header>
    </div>
  );
}

export default App;
