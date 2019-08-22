import React, { useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Typography from '@material-ui/core/Typography';

function App() {
  const [takePreviews, setTakePreviews] = useState(false);
  const [takeMajor, setTakeMajor] = useState(false);
  const [repo, setRepo] = useState('');

  async function buttonHandler() {
    await fetch(
      'api/update/trigger',
      {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          takePreviews: takePreviews,
          takeMajor: takeMajor,
          repo: repo
        })
      });
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
          value={repo}
          onChange={event => setRepo(event.target.value)}
        />

        <FormControlLabel
          control={<Checkbox color="primary" value={takePreviews} onChange={() => setTakePreviews(!takePreviews)} />}
          label="Include previews"
          style={{ color: "black" }}
        />
        <FormControlLabel
          control={<Checkbox color="primary" value={takeMajor} onChange={() => setTakeMajor(!takeMajor)} />}
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
