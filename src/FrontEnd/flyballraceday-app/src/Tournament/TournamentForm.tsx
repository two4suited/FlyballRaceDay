// components/TournamentForm.tsx

import React, { useState } from 'react';
import { Tournament } from './models/Tournament';
import * as TournamentService from './services/TournamentService';

const TournamentForm: React.FC = () => {
    const [tournament, setTournament] = useState<Omit<Tournament, 'Id'>>({
        EventName: '',
        StartDate: new Date(),
        EndDate: new Date(),
        NumberOfLanes: 2
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;

        setTournament(prev => ({
            ...prev,
            [name]: name === 'NumberOfLanes' ? parseInt(value) : (name === 'StartDate' || name === 'EndDate') 
            ? new Date(value) 
            : value
        }));
    };

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            await TournamentService.createTournament(tournament as Tournament);
            alert('Tournament created successfully');
            setTournament({
                EventName: '',
                StartDate: new Date(),
                EndDate: new Date(),
                NumberOfLanes: 0
            });
        } catch (error) {
            alert('Failed to create tournament. Please try again.');
            console.error(error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Event Name:</label>
                <input type="text" name="EventName" value={tournament.EventName} onChange={handleChange} />
            </div>

            <div>
                <label>Start Date:</label>
                <input type="date" name="StartDate" value={tournament.StartDate.toISOString().split('T')[0]} onChange={handleChange} />
            </div>

            <div>
                <label>End Date:</label>
                <input type="date" name="EndDate" value={tournament.EndDate.toISOString().split('T')[0]} onChange={handleChange} />
            </div>

            <div>
                <label>Number of Lanes:</label>
                <input type="number" name="NumberOfLanes" value={tournament.NumberOfLanes} onChange={handleChange} />
            </div>

            <div>
                <button type="submit">Create Tournament</button>
            </div>
        </form>
    );
}

export default TournamentForm;
