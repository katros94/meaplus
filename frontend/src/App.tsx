import { useState } from 'react';
import './App.css';
import { Button, FormControl, FormHelperText, Input, InputLabel} from '@mui/material';
import axios from 'axios';

export interface IExternalParticipant {
  email: string;
}

export interface IMessage {
  external_participants: IExternalParticipant[];
  subject: string;
  body: string;
}

export interface MessagepFormState  {
  subject: string;
  body: string;
  email: string;
}

function App() {
  const API = "https://localhost:7277/api/Meaplus";
  

  const [formData, setFormData] = useState<MessagepFormState> ({
    subject: '',
    body: '',
    email: '', 
  })

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const {name, value} = e.target;
    setFormData(prevData => ({...prevData, [name]: value}))
  }

  const handleSubmit = async (e: React.ChangeEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      var message: IMessage = {
        subject: formData.subject,
        body: formData.body,
        external_participants: [{email:formData.email}]
        
      }
      const response = await axios.post(API, message);
      console.log(response);
    }catch (error) {
      console.error(error);
    }
  }
  
  return (
    <div className="App">
     <form onSubmit={handleSubmit} className='form'>
      <FormControl sx={{ width: '40ch' }}>
          <InputLabel htmlFor="email">E-mail</InputLabel>
          <Input id="email" name="email" aria-describedby="email-helper" value={formData.email} 
          onChange={handleChange} />
          <FormHelperText id="email">We'll never share your email.</FormHelperText>
        </FormControl>
        <FormControl sx={{ width: '40ch' }}>
            <InputLabel htmlFor="subject">Subject</InputLabel>
            <Input id="subject" name="subject" aria-describedby="subject-helper" value={formData.subject} 
            onChange={handleChange} />
            <FormHelperText id="subject">Subject for the greater good.</FormHelperText>
          </FormControl>
          <FormControl sx={{ width: '40ch'}}>
            <InputLabel htmlFor="bodyText">Write your message here...</InputLabel>
            <Input id="bodyText" name="body" aria-describedby="my-helper-bodyText" value={formData.body} 
            onChange={handleChange} multiline={true}/>
            <FormHelperText id="my-helper-bodyText">Your message is safe with us</FormHelperText>
          </FormControl>
          <Button variant="text" type="submit">Send</Button>
     </form>
    </div>
  );
}

export default App;
