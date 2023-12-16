﻿using infrastructure.DataModels;
using infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Service;
using service.Models.Command;

namespace service.Services;

public class AccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly PasswordHashRepository _passwordHashRepository;
    private readonly UserRepository _userRepository;

    public AccountService(ILogger<AccountService> logger, UserRepository userRepository,
        PasswordHashRepository passwordHashRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashRepository = passwordHashRepository;
    }

    public User? Authenticate(LoginCommandModel model)
    {
        try
        {
            var passwordHash = _passwordHashRepository.GetByEmail(model.Email);
            var hashAlgorithm = PasswordHashAlgorithm.Create(passwordHash.Algorithm);
            var isValid = hashAlgorithm.VerifyHashedPassword(model.Password, passwordHash.Hash, passwordHash.Salt);
            if (isValid) return _userRepository.GetById(passwordHash.UserId);
        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        return null;
    }

    public User Register(RegisterCommandModel model)
    {
        var hashAlgorithm = PasswordHashAlgorithm.Create();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(model.Password, salt);
        var user = _userRepository.Create(
            user_email: model.UserEmail,
            hashed: hash,
            user_role: 1,
            user_name: model.Username,
            first_name: model.Firstname,
            last_name: model.Lastname,
            edu: model.Education,
            birth_date: model.BirthDate,
            profile_photo: model.ProfilePhoto
        );
        _passwordHashRepository.Create(user.Id, hash, salt, hashAlgorithm.GetName());
        return user;
    }
    
    public User? Get(SessionData data)
    {
        return _userRepository.GetById(data.UserId);
    }
    
    public User? UpdateUser(int userId, UpdateUserCommandModel model)
    {
        return _userRepository.UpdateUser(
            userId: userId,
            newEmail: model.UserEmail,
            newUsername: model.Username,
            newFirstName: model.Firstname,
            newLastName: model.Lastname,
            newEducation: model.Education,
            newBirthDate: model.BirthDate,
            newProfile: model.ProfilePhoto
        );
        
    }
    
    public User? UpdatePassword(int userId, string newPassword)
    {
        var hashAlgorithm = PasswordHashAlgorithm.Create();
        var salt = hashAlgorithm.GenerateSalt();
        var hash = hashAlgorithm.HashPassword(newPassword, salt);
        _passwordHashRepository.Update(userId, hash, salt, hashAlgorithm.GetName());
        return _userRepository.GetById(userId);
    }
    
    /*
    public User Update(SessionData data, UpdateAccountCommandModel model, string? avatarUrl)
    {
        return _userRepository.Update(data.UserId, model.FullName, model.Email, avatarUrl);
    }
    */
}