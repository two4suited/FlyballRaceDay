// components/TournamentList.tsx

import React, { useEffect, useState } from 'react';
import { Tournament } from './models/Tournament';
import * as TournamentService from './services/TournamentService';

const TournamentList: React.FC = () => {
    const [tournaments, setTournaments] = useState<Tournament[]>([]);

    useEffect(() => {
        const fetchTournaments = async () => {
            const fetchedTournaments = await TournamentService.getTournaments();
            setTournaments(fetchedTournaments);
        };

        fetchTournaments();
    }, []);

    const handleDelete = async (id: string) => {
        try {
            await TournamentService.deleteTournament(id);
            setTournaments(tournaments.filter(t => t.Id !== id)); // Remove the deleted tournament from local state
        } catch (error) {
            console.error('Failed to delete tournament', error);
        }
    };

    return (
        <div>
            <h1>Tournaments</h1>
            <table>
                <thead>
                    <tr>           
                        <th>Region</th>
                        <th>Event Name</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                        <th>Number of Lanes</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {tournaments.map(t => (
                        <tr key={t.Id}>
                            <td>{t.Region}</td>                        
                            <td>{t.EventName}</td>
                            <td>{new Date(t.StartDate).toLocaleDateString()}</td>
                            <td>{new Date(t.EndDate).toLocaleDateString()}</td>
                            <td>{t.NumberOfLanes}</td>
                            <td>
                                <button onClick={() => handleDelete(t.Id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TournamentList;
