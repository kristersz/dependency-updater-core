import React, { useState } from 'react';
import './App.css';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Typography from '@material-ui/core/Typography';
import Fab from '@material-ui/core/Fab';
import ModalLoader from './ModalLoader';

function App() {
  const [takePreviews, setTakePreviews] = useState(false);
  const [takeMajor, setTakeMajor] = useState(false);
  const [repo, setRepo] = useState('');
  const [fetching, setFetching] = useState(false);

  async function buttonHandler() {
    setFetching(true);
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
    setFetching(false);
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
          onClick={buttonHandler}
          color="secondary"
          className={(fetching || repo.length === 0) ? null : "ButtonSpin"}
          disabled={fetching || repo.length === 0}
        >
          GO
        </Fab>
        {fetching && <ModalLoader />}
      </header>
    </div>
  );
}

export default App;
