import React, { useState } from 'react';
import './App.css';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Typography from '@material-ui/core/Typography';
import Fab from '@material-ui/core/Fab';

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
          style={{ color: "black" }}
        />

        <Fab
          color="secondary"
          className={repo.length === 0 ? null : "ButtonSpin"}
          disabled={repo.length === 0}
        >
          GO
        </Fab>
      </header>
    </div>
  );
}

export default App;
